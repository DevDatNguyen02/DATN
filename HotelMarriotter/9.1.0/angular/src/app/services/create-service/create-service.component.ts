import { Component, EventEmitter, Injector, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AppComponentBase } from '@shared/app-component-base';
import { ServiceDto, ServiceServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-create-service',
  templateUrl: './create-service.component.html',
  providers: [{ provide: NgForm }]
})
export class CreateServiceComponent extends AppComponentBase implements OnInit {

  closeModal() {
    this.bsModalRef.hide();
  }
  isValidForm(): boolean {
    return this.createForm && this.createForm.valid;
  }

  saving = false;
  service = new ServiceDto();
  // imageUrl: string;
  // // Hàm này được gọi khi người dùng chọn một hình ảnh
  // onFileSelected(event: any): void {
  //   if (event.target.files && event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //     this.service.imageFile = file; // Gán hình ảnh đã chọn vào thuộc tính imageFile của room
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
  public _serviceService: ServiceServiceProxy,
  public bsModalRef: BsModalRef,
  public createForm: NgForm 
  ) {
  super(injector);
  }

  ngOnInit(): void {
  }


  save(): void {
  this.saving = true;
  this._serviceService.addService(this.service).subscribe(
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

