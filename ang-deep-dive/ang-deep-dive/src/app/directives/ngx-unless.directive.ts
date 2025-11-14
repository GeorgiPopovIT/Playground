import { Directive, TemplateRef, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[ngxUnless]'
})
export class NgxUnlessDirective {

  constructor(private templateRef: TemplateRef<any>,
              private viewContainer: ViewContainerRef) { 

  }

}
