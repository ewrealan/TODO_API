import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from 'src/app/services/task.service';
import { Task } from 'src/app/models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-list.component.html',
  styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];

  constructor(private taskService: TaskService) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getTasks().subscribe({
      next: (data) => {
        this.tasks = data;
      },
      error: (err) => {
        console.error('Görevler alınırken hata oluştu:', err);
      }
    });
  }

  deleteTask(id: number): void {
    this.taskService.deleteTask(id).subscribe({
      next: () => {
        this.tasks = this.tasks.filter(task => task.id !== id);
      },
      error: (err) => {
        console.error('Silme işlemi sırasında hata:', err);
      }
    });
  }

  toggleCompletion(task: Task): void {
    const updatedTask = { ...task, isCompleted: !task.isCompleted };

    this.taskService.updateTask(updatedTask).subscribe({
      next: (updated) => {
        if (updated) {
          const index = this.tasks.findIndex(t => t.id === updated.id);
          if (index !== -1) {
            this.tasks[index] = updated;
          }
        } else {
          console.error('Güncelleme sonucu geçersiz (null) döndü');
        }
      },
      error: (err) => {
        console.error('Güncelleme sırasında hata:', err);
      }
    });
  }
}
