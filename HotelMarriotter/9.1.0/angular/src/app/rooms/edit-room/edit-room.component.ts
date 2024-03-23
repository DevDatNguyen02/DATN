import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-edit-room',
  templateUrl: './edit-room.component.html',
  providers: [{provide: NgForm}]
})
export class EditRoomComponent {

}
