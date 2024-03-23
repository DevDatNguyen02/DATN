import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { GuestDto, GuestServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-edit-guest',
  templateUrl: './edit-guest.component.html',
  providers: [{provide: NgForm}]
})
export class EditGuestComponent extends AppComponentBase implements OnInit {
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.editForm && this.editForm.valid;
  }
  saving = false;
  guest = new GuestDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _guestService: GuestServiceProxy,
    public bsModalRef: BsModalRef,
    public editForm: NgForm 
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._guestService.getGuestById(this.id).subscribe((result) => {
      this.guest = result;
    });
  }

  save(): void {
    this.saving = true;

    this._guestService.updateGuest(this.guest).subscribe(
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

