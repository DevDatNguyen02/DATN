import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { TypeRoomDto, TypeRoomServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-typeroom',
  templateUrl: './create-typeroom.component.html',
  providers: [{provide: NgForm}]
})
export class CreateTyperoomComponent extends AppComponentBase implements OnInit {
  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.createForm && this.createForm.valid;
  }

        saving = false;
        typeroom = new TypeRoomDto();

        @Output() onSave = new EventEmitter<any>();

        constructor(
        injector: Injector,
        public _typeroomService: TypeRoomServiceProxy,
        public bsModalRef: BsModalRef,
        public createForm: NgForm 
        ) {
        super(injector);
        }

        ngOnInit(): void {
        }


        save(): void {
        this.saving = true;
        this._typeroomService.addTypeRoom(this.typeroom).subscribe(
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

