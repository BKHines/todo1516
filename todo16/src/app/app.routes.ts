import { Routes } from '@angular/router';

export const routes: Routes = [
    { path: 'list', loadComponent: () => import('../app/components/todo/list/list.component')},
    { path: 'entry', loadComponent: () => import('../app/components/todo/entry/entry.component')},
    { path: '', redirectTo: 'list', pathMatch: 'full' }
];
