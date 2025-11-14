import { AfterContentChecked, AfterViewChecked, AfterViewInit, Component, ElementRef, inject, Injector, Input, OnChanges, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HomeComponent } from "./home/home.component";
import { personData } from '../common/data';
import { Person } from '../common/Person';
import { NgxUnlessDirective } from '../app/directives/ngx-unless.directive';
import { ReversePipe } from './pipes/reverse.pipe';
@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HomeComponent, ReversePipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit, OnChanges,AfterContentChecked, AfterViewChecked, AfterViewInit {
  titleT =  "dads";
  dataPerson : Person[] = personData;
  outputMessage! : string;

  @ViewChild('home')
  inputRef!: ElementRef;

  private injector = inject(Injector);

  constructor() { 
  }
  ngAfterViewChecked(): void {
    console.log('AfterViewChecked called');
  }
  ngAfterContentChecked(): void {
    console.log('AfterContentChecked called');
  }
ngOnChanges(changes: SimpleChanges): void {
    console.log('OnChanges called');
  }
ngOnInit(): void {
    console.log('OnInit called');
  }

  receiveOutputMessage(message: string) {
    this.outputMessage = message;
  }

  ngAfterViewInit(){
    //this.title = "ViewChild Example";
  }
}
