import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { StaffDto, StaffServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-staff',
  templateUrl: './create-staff.component.html',
  providers: [{provide: NgForm}]
})
export class CreateStaffComponent extends AppComponentBase implements OnInit {
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.createForm && this.createForm.valid;
  }

        saving = false;
        staff = new StaffDto();

        @Output() onSave = new EventEmitter<any>();

        constructor(
        injector: Injector,
        public _staffService: StaffServiceProxy,
        public bsModalRef: BsModalRef,
        public createForm: NgForm 
        ) {
        super(injector);
        }

        ngOnInit(): void {
        }


        save(): void {
        this.saving = true;
        this._staffService.addStaff(this.staff).subscribe(
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
