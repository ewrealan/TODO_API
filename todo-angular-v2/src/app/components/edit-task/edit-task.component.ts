import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../services/task.service';
import { CategoryService } from '../../services/category.service';
import { Task } from '../../models/task.model';
import { Category } from '../../models/category.model';

@Component({
  selector: 'app-edit-task',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './edit-task.component.html',
  styleUrls: ['./edit-task.component.css']
})
export class EditTaskComponent implements OnInit {
  taskId: number = 0;
  task: any = {
    title: '',
    isCompleted: false,
    categoryName: ''
  };

  categories: Category[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private categoryService: CategoryService
  ) {}

  ngOnInit(): void {
    this.taskId = Number(this.route.snapshot.paramMap.get('id'));

    // Kategorileri yükle
    this.categoryService.getCategories().subscribe({
      next: (data) => this.categories = data
    });

    // Görevi getir
    this.taskService.getTaskById(this.taskId).subscribe({
      next: (data) => {
        this.task = {
          title: data.title,
          isCompleted: data.isCompleted,
          categoryName: data.categoryName
        };
      },
      error: (err) => {
        alert('Görev bulunamadı.');
        this.router.navigate(['/']);
      }
    });
  }

  updateTask(): void {
    this.taskService.updateTask({ id: this.taskId, ...this.task }).subscribe({
      next: () => {
        alert('Görev güncellendi!');
        this.router.navigate(['/']);
      },
      error: () => {
        alert('Güncelleme başarısız.');
      }
    });
  }
}
