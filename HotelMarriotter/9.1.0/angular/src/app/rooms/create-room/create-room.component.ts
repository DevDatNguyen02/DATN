import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { RoomDto, RoomServiceProxy, TypeRoomDto, TypeRoomServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-room',
  templateUrl: './create-room.component.html',
  providers: [{provide: NgForm}]
})
export class CreateRoomComponent extends AppComponentBase implements OnInit {

        closeModal() {
          this.bsModalRef.hide();
        }
        isValidForm(): boolean {
          return this.createForm && this.createForm.valid;
        }
        ngOnInit(): void {
          this.getTypeRooms();
        }
      
        getTypeRooms(): void {
          const pageNumber = 1; // Chỉ lấy trang đầu tiên
          const pageSize = 10; 
          this.typeRoomService.getAllTypeRooms(pageNumber,pageSize).subscribe(result => {
            this.typeRooms = result;
          });
        }

        saving = false;
        room = new RoomDto();
        typeRooms: TypeRoomDto[];
        // imageUrl: string;
        // // Hàm này được gọi khi người dùng chọn một hình ảnh
        // onFileSelected(event: any): void {
        //   if (event.target.files && event.target.files.length > 0) {
        //     const file = event.target.files[0];
        //     this.room.imageFile = file; // Gán hình ảnh đã chọn vào thuộc tính imageFile của room
        //     const reader = new FileReader();
        //     reader.readAsDataURL(file);
        //     reader.onload = () => {
        //       this.imageUrl = reader.result as string; // Lưu đường dẫn của hình ảnh để hiển thị trên giao diện
        //     };
        //   }
        // }

        @Output() onSave = new EventEmitter<any>();

        constructor(
        injector: Injector,
        public _roomService: RoomServiceProxy,
        public typeRoomService: TypeRoomServiceProxy,
        public bsModalRef: BsModalRef,
        public createForm: NgForm 
        ) {
        super(injector);
        }


        save(): void {
        this.saving = true;
        this._roomService.addRoom(this.room).subscribe(
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

