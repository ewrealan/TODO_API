export interface Task {
  id: number;
  title: string;
  description?: string;
  dueDate?: string;
  priorityLevel: number; // required çünkü DTO'da zorunlu
  isCompleted: boolean;

  // Backend'den DTO ile gelen bilgiler
  categoryName: string;   // ✅ required
  categoryColor?: string; // opsiyonel
}
