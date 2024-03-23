import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { ServiceDto, ServiceServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-service',
  templateUrl: './edit-service.component.html',
  providers: [{provide: NgForm}]
})
export class EditServiceComponent extends AppComponentBase implements OnInit {
priceModel: any;
pricelEl: any;
staff: any;
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.editForm && this.editForm.valid;
  }
  saving = false;
  service = new ServiceDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _serviceService: ServiceServiceProxy,
    public bsModalRef: BsModalRef,
    public editForm: NgForm 
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._serviceService.getServiceById(this.id).subscribe((result) => {
      this.service = result;
    });
  }

  save(): void {
    this.saving = true;

    this._serviceService.updateService(this.service).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}

