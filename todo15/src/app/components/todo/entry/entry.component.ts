import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  templateUrl: './entry.component.html',
  styleUrls: ['./entry.component.scss']
})
export class EntryComponent {

  constructor(private router: Router) {}

  cancel() {
    this.router.navigateByUrl('/');
  }
}
