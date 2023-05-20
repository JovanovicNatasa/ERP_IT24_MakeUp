import { DOCUMENT } from '@angular/common';
import { AfterViewInit, Directive, ElementRef, EventEmitter, Inject, Input, OnDestroy, Output } from '@angular/core';
import { Subscription, filter, fromEvent } from 'rxjs';

@Directive({ selector: '[clickOutside]' })
export class ClickOutsideDirective implements AfterViewInit, OnDestroy {
  @Input('clickOutsideId') menuId!: string;
  @Output('clickOutside') clickOutside: EventEmitter<string> = new EventEmitter<string>();
  @Output('clickInside') clickInside: EventEmitter<string> = new EventEmitter<string>();
  documentClickSubscription: Subscription | undefined;

  constructor(private element: ElementRef, @Inject(DOCUMENT) private document: Document) {}

  ngAfterViewInit(): void {
    this.documentClickSubscription = fromEvent(this.document, 'click')
      .subscribe((event) => {
        if (!this.isInside(event.target as HTMLElement)) {
          this.clickOutside.emit(this.menuId);
        } else {
          this.clickInside.emit(this.menuId);
        }
      });
  }

  ngOnDestroy(): void {
    this.documentClickSubscription?.unsubscribe();
  }

  isInside(elementToCheck: HTMLElement): boolean {
    return elementToCheck === this.element.nativeElement || this.element.nativeElement.contains(elementToCheck);
  }
}
