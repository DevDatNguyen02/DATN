import { Component, OnInit } from '@angular/core';
import { EditServiceComponent } from '@app/services/edit-service/edit-service.component';
import { CreateServiceComponent } from '@app/services/create-service/create-service.component';
import { ServiceDto, ServiceServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-services',
  templateUrl: './services.component.html',
  providers: [{provide: NgForm}]
})
export class ServicesComponent implements OnInit{
  services: ServiceDto[] = [];
  _modalService: any;
  notify: any;
  translate: any;
  searchKeyword: string = '';
  advancedFiltersVisible: any;
  isActive: any;
  isTableLoading: any;
  pageSize: any;
  pageNumber: any;
  totalItems: any;

  imageUrl: string | undefined;

  // Hàm xử lý sự kiện khi người dùng chọn ảnh
  onFileSelected(event: any): void {
    const file: File = event.target.files[0]; // Lấy thông tin của tệp ảnh được chọn
    if (file) {
      // Chuyển đổi tệp ảnh thành URL sử dụng FileReader
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageUrl = reader.result as string; // Lưu URL của ảnh vào biến imageUrl
      };
    }
  }

  constructor(private serviceService: ServiceServiceProxy, private modalService: BsModalService) { }
 
  ngOnInit(): void {
    this.getAllServices();
  }

  getAllServices(): void {
    this.serviceService.getAllServices().subscribe(
      (services: ServiceDto[]) => {
        this.services = services;
      },
      (error: any) => {
        console.error('Error fetching staffs:', error);
      }
    );
  }

  createServices(): void {
    this.showCreateOrEditServiceDialog();
  }

  editStaff(service: ServiceDto): void {
    this.showCreateOrEditServiceDialog(service.id);
  }
  
  
  private showCreateOrEditServiceDialog(id?: number): void {
    let createOrEditServiceDialog: BsModalRef;
    if (!id) {
      createOrEditServiceDialog = this.modalService.show(
        CreateServiceComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditServiceDialog = this.modalService.show(
        EditServiceComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditServiceDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  
  deleteService(serviceId: number) {

    abp.message.confirm(
      'Service will be deleted.', 
      undefined, 
      (result: boolean) => {
        if (result) {
          this.serviceService.deleteService(serviceId).subscribe(
            () => {
              this.refresh();
              abp.notify.success('Successfully deleted service');
            },
            (error) => {
              console.error('Error deleting service', error);
            }
          );  
        }
      }
    );
  
  }


  searchServices(): void {
    this.serviceService.searchServices(this.searchKeyword).subscribe(
      (services: ServiceDto[]) => {
        this.services = services;
      },
      (error: any) => {
        console.error('Error searching services:', error);
      }
    );
  }

  resetSearch(): void {
    this.searchKeyword = '';
    this.getAllServices();
  }
 
  
  
  refresh(): void {
    this.getAllServices();
  }


}
