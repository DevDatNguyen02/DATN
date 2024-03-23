import { Component, OnInit } from '@angular/core';
import { RoomDto, RoomServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateRoomComponent } from './create-room/create-room.component';
import { EditRoomComponent } from './edit-room/edit-room.component';

@Component({
  selector: 'app-rooms',
  templateUrl: './rooms.component.html',

})
export class RoomsComponent implements OnInit{

  rooms: RoomDto[] = [];
  _modalService: any;
  notify: any;
  translate: any;
  searchKeyword: string = '';
    advancedFiltersVisible: any;
    isActive: any;
    isTableLoading: any;
    services: any;
    pageSize: any;
    pageNumber: any;
    totalItems: any;

  constructor(private roomService: RoomServiceProxy, private modalService: BsModalService) { }
 
  ngOnInit(): void {
    this.getAllRooms();
  }

  getAllRooms(): void {
    this.roomService.getAllRooms().subscribe(
      (rooms: RoomDto[]) => {
        this.rooms = rooms;
      },
      (error: any) => {
        console.error('Error fetching rooms:', error);
      }
    );
  }

  createRoom(): void {
    this.showCreateOrEditRoomDialog();
  }

  editRoom(room: RoomDto): void {
    this.showCreateOrEditRoomDialog(room.id);
  }
  
  
  private showCreateOrEditRoomDialog(id?: number): void {
    let createOrEditRoomDialog: BsModalRef;
    if (!id) {
      createOrEditRoomDialog = this.modalService.show(
        CreateRoomComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditRoomDialog = this.modalService.show(
        EditRoomComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditRoomDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  
  deleteRoom(roomId: number) {

    abp.message.confirm(
      'Room will be deleted.', 
      undefined, 
      (result: boolean) => {
        if (result) {
          this.roomService.deleteRoom(roomId).subscribe(
            () => {
              this.refresh();
              abp.notify.success('Successfully deleted room');
            },
            (error) => {
              console.error('Error deleting room', error);
            }
          );  
        }
      }
    );
  
  }


  searchRooms(): void {
    this.roomService.searchRooms(this.searchKeyword).subscribe(
      (rooms: RoomDto[]) => {
        this.rooms = rooms;
      },
      (error: any) => {
        console.error('Error searching rooms:', error);
      }
    );
  }

  resetSearch(): void {
    this.searchKeyword = '';
    this.getAllRooms();
  }
 
  
  
  refresh(): void {
    this.getAllRooms();
  }


}

