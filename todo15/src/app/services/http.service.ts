import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TodoItem } from '../entities/todoitem';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  private baseApiUrl = 'http://localhost:15016';
  public userid?: string;

  constructor(private http: HttpClient) {
  }

  getTodoItems(): Observable<TodoItem[]> {
    return this.http.get<TodoItem[]>(`${this.baseApiUrl}/api/Todo/GetTodos`);
  }

  getTodoItem(id: number): Observable<TodoItem> {
    let params: HttpParams = new HttpParams().set('id', id);
    return this.http.get<TodoItem>(`${this.baseApiUrl}/api/Todo/GetTodo`, { params });
  }

  addTodoItem(item: TodoItem): Observable<number> {
    return this.http.post<number>(`${this.baseApiUrl}/api/Todo/AddTodo`, item);
  }

  updateTodoItem(id: number, item: TodoItem): Observable<boolean> {
    let params: HttpParams = new HttpParams().set('id', id);
    return this.http.put<boolean>(`${this.baseApiUrl}/api/Todo/UpdateTodo`, item, { params });
  }

  deleteTodoItem(id: number): Observable<boolean> {
    let params: HttpParams = new HttpParams().set('id', id);
    return this.http.delete<boolean>(`${this.baseApiUrl}/api/Todo/DeleteTodo`, { params });
  }
}
