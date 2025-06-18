export interface Task {
    id: number;
    title: string;
    description: string;
    categoryId: number;
    categoryName?: string;     // opsiyonel, sadece DTO'dan gelirse dolu olur
    categoryColor?: string;    // opsiyonel, sadece DTO'dan gelirse dolu olur
    dueDate: string;           // ISO 8601 formatýnda tarih (örnek: "2025-06-20T00:00:00")
    priorityLevel: number;
    isCompleted: boolean;
    createdDate?: string;      // opsiyonel, sadece getirilenlerde olabilir
}
