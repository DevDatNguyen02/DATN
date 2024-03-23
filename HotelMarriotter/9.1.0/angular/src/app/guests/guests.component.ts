import { Component, OnInit } from '@angular/core';
import { GuestDto, GuestServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateGuestComponent } from './create-guest/create-guest.component';
import { EditGuestComponent } from './edit-guest/edit-guest.component';

@Component({
  selector: 'app-guests',
  templateUrl: './guests.component.html',
})
export class GuestsComponent implements OnInit{
editRoom(_t69: any) {
throw new Error('Method not implemented.');
}
deleteRoom(arg0: any) {
throw new Error('Method not implemented.');
}

  guests: GuestDto[] = [];
  _modalService: any;
  notify: any;
  translate: any;
  searchKeyword: string = '';
advancedFiltersVisible: any;
isActive: any;
isTableLoading: any;
rooms: any;
pageSize: any;
pageNumber: any;
totalItems: any;

  constructor(private guestService: GuestServiceProxy, private modalService: BsModalService) { }
 
  ngOnInit(): void {
    this.getAllGuests();
  }

  getAllGuests(): void {
    this.guestService.getAllGuests().subscribe(
      (guests: GuestDto[]) => {
        this.guests = guests;
      },
      (error: any) => {
        console.error('Error fetching staffs:', error);
      }
    );
  }

  createGuest(): void {
    this.showCreateOrEditGuestDialog();
  }

  editGuest(guest: GuestDto): void {
    this.showCreateOrEditGuestDialog(guest.id);
  }
  
  
  private showCreateOrEditGuestDialog(id?: number): void {
    let createOrEditGuestDialog: BsModalRef;
    if (!id) {
      createOrEditGuestDialog = this.modalService.show(
        CreateGuestComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditGuestDialog = this.modalService.show(
        EditGuestComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditGuestDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  
  deleteGuest(guestId: number) {

    abp.message.confirm(
      'Staff will be deleted.', 
      undefined, 
      (result: boolean) => {
        if (result) {
          this.guestService.deleteGuest(guestId).subscribe(
            () => {
              this.refresh();
              abp.notify.success('Successfully deleted guest');
            },
            (error) => {
              console.error('Error deleting guest', error);
            }
          );  
        }
      }
    );
  
  }


  searchGuests(): void {
    this.guestService.searchGuets(this.searchKeyword).subscribe(
      (guests: GuestDto[]) => {
        this.guests = guests;
      },
      (error: any) => {
        console.error('Error searching guests:', error);
      }
    );
  }

  resetSearch(): void {
    this.searchKeyword = '';
    this.getAllGuests();
  }
 
  
  
  refresh(): void {
    this.getAllGuests();
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
