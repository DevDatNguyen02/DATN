import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import{StaffServiceProxy,StaffDto} from '@shared/service-proxies/service-proxies';
import { CreateStaffComponent } from './create-staff/create-staff.component';
import { EditStaffComponent } from './edit-staff/edit-staff.component';
@Component({
  selector: 'app-staffs',
  templateUrl: './staffs.component.html',
})
export class StaffsComponent implements OnInit{

  staffs: StaffDto[] = [];
  _modalService: any;
  notify: any;
  translate: any;
  searchKeyword: string = '';

  constructor(private staffService: StaffServiceProxy, private modalService: BsModalService) { }
 
  ngOnInit(): void {
    this.getAllStaffs();
  }
  

  getAllStaffs(): void {
    this.staffService.getAllStaffs().subscribe(
      (staffs: StaffDto[]) => {
        this.staffs = staffs;
      },
      (error: any) => {
        console.error('Error fetching staffs:', error);
      }
    );
  }

  createStaff(): void {
    this.showCreateOrEditStaffDialog();
  }

  editStaff(staff: StaffDto): void {
    this.showCreateOrEditStaffDialog(staff.id);
  }
  
  
  private showCreateOrEditStaffDialog(id?: number): void {
    let createOrEditStaffDialog: BsModalRef;
    if (!id) {
      createOrEditStaffDialog = this.modalService.show(
        CreateStaffComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditStaffDialog = this.modalService.show(
        EditStaffComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditStaffDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  
  deleteStaff(staffId: number) {

    abp.message.confirm(
      'Staff will be deleted.', 
      undefined, 
      (result: boolean) => {
        if (result) {
          this.staffService.deleteStaff(staffId).subscribe(
            () => {
              this.refresh();
              abp.notify.success('Successfully deleted staff');
            },
            (error) => {
              console.error('Error deleting staff', error);
            }
          );  
        }
      }
    );
  
  }


  searchStaffs(): void {
    this.staffService.searchStaffs(this.searchKeyword).subscribe(
      (staffs: StaffDto[]) => {
        this.staffs = staffs;
      },
      (error: any) => {
        console.error('Error searching staffs:', error);
      }
    );
  }

  resetSearch(): void {
    this.searchKeyword = '';
    this.getAllStaffs();
  }
 
  
  
  refresh(): void {
    this.getAllStaffs();
  }
  getGenderString(gender: number): string {
    switch (gender) {
      case 0:
        return 'Male';
      case 1:
        return 'Female';
      case 2:
        return 'Other';
      default:
        return 'Unknown';
    }
  }

}
