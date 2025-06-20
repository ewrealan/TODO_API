import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task } from '../models/task.model';

@Injectable({ providedIn: 'root' })
export class TaskService {
  private apiUrl = 'https://localhost:7201/api/tasks';

  constructor(private http: HttpClient) {}

  // ✅ Tüm görevleri getir
  getTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  // ✅ Belirli bir görevi getir
  getTaskById(id: number): Observable<Task> {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }

  // ✅ Yeni görev ekle
  addTask(task: Task): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, task);
  }

  // ✅ Görevi güncelle
  updateTask(task: Task): Observable<Task> {
    return this.http.put<Task>(`${this.apiUrl}/${task.id}`, task);
  }

  // ✅ Görev sil
  deleteTask(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // ✅ Görevi tamamlandı olarak işaretle
  toggleComplete(id: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}/complete`, {});
  }
}
