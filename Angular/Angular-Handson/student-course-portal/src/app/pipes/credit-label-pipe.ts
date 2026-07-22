import { Pipe, PipeTransform } from '@angular/core';

// Pipes are pure by default — only re-run when input reference changes
// Set pure: false only when you need to re-run on mutable data changes (impacts performance)
@Pipe({
  name: 'creditLabel',
  standalone: false
})
export class CreditLabelPipe implements PipeTransform {
  transform(credits: number | null | undefined): string {
    if (credits === null || credits === undefined || credits === 0) return 'No Credits';
    return credits === 1 ? '1 Credit' : `${credits} Credits`;
  }
}
