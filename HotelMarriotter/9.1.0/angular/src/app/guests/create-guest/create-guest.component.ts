import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { GuestDto, GuestServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-guest',
  providers: [{provide: NgForm}],
  templateUrl: './create-guest.component.html',
})
export class CreateGuestComponent extends AppComponentBase implements OnInit {
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.createForm && this.createForm.valid;
  }

        saving = false;
        guest = new GuestDto();

        @Output() onSave = new EventEmitter<any>();

        constructor(
        injector: Injector,
        public _guestService: GuestServiceProxy,
        public bsModalRef: BsModalRef,
        public createForm: NgForm 
        ) {
        super(injector);
        }

        ngOnInit(): void {
        }


        save(): void {
        this.saving = true;
        this._guestService.addGuest(this.guest).subscribe(
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
