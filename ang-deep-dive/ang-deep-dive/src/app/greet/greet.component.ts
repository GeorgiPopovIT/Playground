import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-greet',
  imports: [],
  template: `<h1>Hello, {{name}}!</h1>`,
  styles: []
})
export class GreetComponent {
 @Input()
  name: string = 'World';
}
