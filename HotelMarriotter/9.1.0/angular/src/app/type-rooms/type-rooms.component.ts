import { Component, OnInit } from '@angular/core';
import { TypeRoomDto, TypeRoomServiceProxy } from '@shared/service-proxies/service-proxies';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { CreateTyperoomComponent } from './create-typeroom/create-typeroom.component';
import { EditTyperoomComponent } from './edit-typeroom/edit-typeroom.component';
import { catchError, of } from 'rxjs';

@Component({
  selector: 'app-type-rooms',
  templateUrl: './type-rooms.component.html',
})
export class TypeRoomsComponent implements OnInit {
  typerooms: TypeRoomDto[] = [];
  searchKeyword: string = '';
  advancedFiltersVisible: boolean = false;
  isActive: boolean | null = null;
  isTableLoading: boolean = false;
  pageNumber: number = 1;
  pageSize: number = 1;
  totalItems: number = 0;
  
  constructor(private typeroomService: TypeRoomServiceProxy, private modalService: BsModalService) {}

  ngOnInit(): void {
    this.getAllTypeRooms();
  }

  getAllTypeRooms(): void {
    this.isTableLoading = true;
    this.typeroomService.getAllTypeRooms(this.pageNumber, this.pageSize).subscribe(
      (result: any) => {
        this.typerooms = result.items;
        this.totalItems = result.totalCount;
        this.isTableLoading = false;
      },
      (error: any) => {
        console.error('Error fetching type rooms:', error);
        this.isTableLoading = false;
      }
    );
  }

  createTypeRoom(): void {
    this.showCreateOrEditTypeRoomDialog();
  }

  editTypeRoom(typeroom: TypeRoomDto): void {
    this.showCreateOrEditTypeRoomDialog(typeroom.id);
  }

  private showCreateOrEditTypeRoomDialog(id?: number): void {
    let createOrEditTypeRoomDialog: BsModalRef;
    if (!id) {
      createOrEditTypeRoomDialog = this.modalService.show(CreateTyperoomComponent, {
        class: 'modal-lg',
      });
    } else {
      createOrEditTypeRoomDialog = this.modalService.show(EditTyperoomComponent, {
        class: 'modal-lg',
        initialState: {
          id: id,
        },
      });
    }

    createOrEditTypeRoomDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }

  deleteTypeRoom(typeroomId: number): void {
    abp.message.confirm('Type Room will be deleted.', undefined, (result: boolean) => {
      if (result) {
        this.typeroomService.deleteTypeRoom(typeroomId).subscribe(
          () => {
            this.refresh();
            abp.notify.success('Successfully deleted type room');
          },
          (error) => {
            console.error('Error deleting type room', error);
          }
        );
      }
    });
  }

  searchTypeRooms(): void {
    this.getAllTypeRooms();
  }

  resetSearch(): void {
    this.searchKeyword = '';
    this.isActive = null;
    this.getAllTypeRooms();
  }

  onPageChange(page: number): void {
    this.pageNumber = page;
    this.getAllTypeRooms();
  }

  onPageSizeChange(event: any): void {
    this.pageSize = event.target.value;
    this.pageNumber = 1; // Reset to first page when changing page size
    this.getAllTypeRooms();
  }


  refresh(): void {
    this.getAllTypeRooms();
  }
}
