import { Component, OnInit } from '@angular/core';
import { TodoItem } from 'src/app/entities/todoitem';
import { HttpService } from 'src/app/services/http.service';
import { UtilityService } from 'src/app/services/utility.service';

@Component({
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {
  private log1counter: number;
  private log2counter: number;

  public todotype: string;
  public items: TodoItem[];
  private _userid?: string | undefined;
  public get userid(): string | undefined {
    return this._userid;
  }
  public set userid(value: string | undefined) {
    this._userid = value;
    this.httpSvc.userid = value;
  }

  constructor(private httpSvc: HttpService, private utilitySvc: UtilityService) {
    this.log1counter = 0;
    this.log2counter = 0;
    this.todotype = '';
    this.items = [];
    this.userid = this.httpSvc.userid;
  }

  ngOnInit(): void {
    this.refreshItems();
  }

  refreshItems() {
    this.httpSvc.getTodoItems().subscribe((res) => {
      let _items = res;
      _items.push(...this.items);
      this.items = _items.filter(this.utilitySvc.uniqueInArrayCheck);
    });
  }

  getItems(): TodoItem[] {
    console.log(`Page called getItems ${++this.log1counter} times`);
    return this.items;
  }

  addItemToEnter() {
    let _tdi: TodoItem = { description: '', order: -1, status: 'unsynced', userident: this.httpSvc.userid, editable: true, itemtype: this.todotype };
    this.items.push(_tdi);
  }

  getEditable(id?: number) {
    console.log(`Page called getEditable ${++this.log2counter} times`);
    return id ? this.items.find(a => a.id == id)?.editable : true;
  }

  editItem(tdi: TodoItem) {
    tdi.editable = true;
  }

  deleteItem(tdi: TodoItem) {
    if (tdi.id) {
      this.httpSvc.deleteTodoItem(tdi.id).subscribe((res) => {
        this.items = this.items.filter(a => a != tdi);
      });  
    } else {
      this.items = this.items.filter(a => a != tdi);
    }
  }

  commitItem(tdi: TodoItem) {
    if (tdi.id && tdi.id > -1) {
      this.httpSvc.updateTodoItem(tdi.id, tdi).subscribe((res) => {
        if (res) {
          tdi.editable = false;
          tdi.status = 'synced';
        }
      });
    } else {
      tdi.id = -1;
      tdi.updated = '';
      tdi.userident = this.userid;
      this.httpSvc.addTodoItem(tdi).subscribe((res) => {
        if (res > 0) {
          this.httpSvc.getTodoItem(res).subscribe((_tdires) => {
            tdi.id = _tdires.id;
            tdi.updated = _tdires.updated;
            tdi.status = _tdires.status;
          });
        }
      });
    }
  }
}
