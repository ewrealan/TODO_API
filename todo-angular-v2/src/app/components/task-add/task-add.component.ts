import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { CategoryService } from '../../services/category.service';
import { Task } from '../../models/task.model';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-task-add',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './task-add.component.html',
  styleUrls: ['./task-add.component.css']
})
export class TaskAddComponent implements OnInit {
  newTask: Task = {
    id: 0,
    title: '',
    description: '',
    dueDate: '',
    priorityLevel: 1,
    categoryName: '',
    isCompleted: false
  };

  categories: Category[] = [];

  constructor(
    private taskService: TaskService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe({
      next: (data) => this.categories = data,
      error: (err) => console.error('Kategori alınamadı:', err)
    });
  }

  addTask(): void {
    if (!this.newTask.title.trim() || !this.newTask.categoryName) return;

    this.taskService.addTask(this.newTask).subscribe({
      next: (createdTask) => {
        console.log('Yeni görev eklendi:', createdTask);
        this.newTask = {
          id: 0,
          title: '',
          description: '',
          dueDate: '',
          priorityLevel: 1,
          categoryName: '',
          isCompleted: false
        };
      },
      error: (err) => {
        console.error('Görev eklenirken hata:', err);
      }
    });
  }
}
