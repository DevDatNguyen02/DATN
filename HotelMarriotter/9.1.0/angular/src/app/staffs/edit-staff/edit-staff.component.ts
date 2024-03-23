import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { StaffDto, StaffServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-edit-staff',
  templateUrl: './edit-staff.component.html',
  providers: [{provide: NgForm}]
})
export class EditStaffComponent extends AppComponentBase implements OnInit {
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.editForm && this.editForm.valid;
  }
  saving = false;
  staff = new StaffDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _staffService: StaffServiceProxy,
    public bsModalRef: BsModalRef,
    public editForm: NgForm 
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._staffService.getStaffById(this.id).subscribe((result) => {
      this.staff = result;
    });
  }

  save(): void {
    this.saving = true;

    this._staffService.updateStaff(this.staff).subscribe(
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
