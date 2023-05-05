import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  standalone: true,
  imports: [CommonModule],
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.scss']
})
export default class EntryComponent {

  constructor(
    private router: Router
  ) { }

  cancel() {
    this.router.navigateByUrl('/');
  } 
}
