import { Directive, ElementRef, HostListener, Input } from '@angular/core';

@Directive({
  selector: '[appHighlight]',
  standalone: false
})
export class Highlight {
  // Configurable highlight colour — caller can pass: <div appHighlight="lightblue">
  @Input() appHighlight = 'yellow';

  constructor(private el: ElementRef) {}

  // @HostListener binds to host element events — Angular cleans up automatically
  @HostListener('mouseenter')
  onMouseEnter(): void {
    this.el.nativeElement.style.backgroundColor = this.appHighlight;
  }

  @HostListener('mouseleave')
  onMouseLeave(): void {
    this.el.nativeElement.style.backgroundColor = '';
  }
}
