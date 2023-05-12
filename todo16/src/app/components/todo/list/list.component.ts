import { Component, OnInit, effect, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { HttpService } from 'src/app/services/http.service';
import { UtilityService } from 'src/app/services/utility.service';
import { FormsModule } from '@angular/forms';
import { TodoItem } from 'src/app/entities/todoitem';
import { HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, HttpClientModule],
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export default class ListComponent implements OnInit {
  useridlogcounter: number;
  todotypelogcounter: number;
  itemslogcounter: number;
  userid = signal<string>('');
  todotype = signal<string>('');
  items = signal<TodoItem[]>([]);

  constructor(private httpSvc: HttpService, private utilitySvc: UtilityService) {
    this.useridlogcounter = 0;
    this.todotypelogcounter = 0;
    this.itemslogcounter = 0;
    effect(() => {
      console.log(`user id event called ${++this.useridlogcounter} times`);
      this.httpSvc.userid = this.userid();
    });
    effect(() => {
      console.log(`todotype effect called ${++this.todotypelogcounter} times`, this.todotype());
    });
    effect(() => {
      console.log(`items called ${++this.itemslogcounter} times`, this.items());
    });
  }

  ngOnInit(): void {
    this.refreshItems();
  }

  refreshItems() {
    this.httpSvc.getTodoItems().subscribe((res) => {
      let _items = res;
      res.forEach((r) => {
        r.editable = signal(false);
      });
      _items.push(...this.items());
      this.items.set(_items.filter(this.utilitySvc.uniqueInArrayCheck));
    });
  }

  addItemToEnter() {
    let _tdi: TodoItem = { description: '', order: -1, status: 'unsynced', userident: this.httpSvc.userid, itemtype: this.todotype() };
    _tdi.editable = signal(true);
    this.items.mutate(values => values.push(_tdi));
    this.resetOrder();
  }

  editItem(tdi: TodoItem) {
    if (tdi.editable) {
      tdi.editable.set(true);
    }
  }

  deleteItem(tdi: TodoItem) {
    if (tdi.id) {
      this.httpSvc.deleteTodoItem(tdi.id).subscribe((res) => {
        if (res) {
          this.items.set(this.items().filter(a => a != tdi));
          this.resetOrder();
        }
      });
    } else {
      this.items.set(this.items().filter(a => a != tdi));
      this.resetOrder();
    }
  }

  commitItem(tdi: TodoItem) {
    if (tdi.id && tdi.id > -1) {
      this.httpSvc.updateTodoItem(tdi.id, tdi).subscribe((res) => {
        if (res) {
          this.updateFromResponse(tdi);
        }
      });
    } else {
      tdi.id = -1;
      tdi.updated = '';
      tdi.userident = this.userid();
      this.httpSvc.addTodoItem(tdi).subscribe((res) => {
        if (res > -1) {
          this.updateFromResponse(tdi, res);
        }
      });
    }
  }

  private resetOrder() {
    let _itemsToUpdate: TodoItem[] = [];
    this.items.mutate(values =>
      values.forEach((a, i) => {
        if (a.order != i + 1) {
          a.order = i + 1;
          _itemsToUpdate.push(a);
        }
        if (_itemsToUpdate.length > 0) {
          this.httpSvc.updateTodoItems(_itemsToUpdate).subscribe((res) => { });
        }
      })
    );
  }

  private updateFromResponse(tdi: TodoItem, tdid: number = -1) {
    this.httpSvc.getTodoItem(tdi.id && tdi.id > -1 ? tdi.id : tdid).subscribe((_tdires) => {
      this.items.mutate(values => {
        const _a = values.findIndex(a => a.description == tdi.description && a.itemtype == tdi.itemtype);
        values[_a] = _tdires;
        values[_a].editable = signal(false);
      });
    });
  }
}
