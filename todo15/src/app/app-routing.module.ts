import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './components/todo/list/list.component';
import { EntryComponent } from './components/todo/entry/entry.component';

const routes: Routes = [
  { path: 'list', component: ListComponent },
  { path: 'entry', component: EntryComponent },
  { path: '', redirectTo: 'list', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
