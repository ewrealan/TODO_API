import { Routes } from '@angular/router';
import { EditTaskComponent } from './components/edit-task/edit-task.component';

export const routes: Routes = [
  { path: 'edit/:id', component: EditTaskComponent }
];
