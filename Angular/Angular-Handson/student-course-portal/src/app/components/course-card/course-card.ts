import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { Course } from '../../models/course.model';
import { EnrollmentService } from '../../services/enrollment';

@Component({
  selector: 'app-course-card',
  standalone: false,
  templateUrl: './course-card.html',
  styleUrl: './course-card.css'
})
export class CourseCard implements OnChanges {
  // Hands-On 2: @Input — data flows DOWN from parent to child
  @Input() course!: Course;

  // Hands-On 2: @Output — events bubble UP from child to parent
  // EventEmitter<number> is strongly typed — payload is course id (number)
  @Output() enrollRequested = new EventEmitter<number>();

  // Hands-On 3: Toggle card expanded state
  isExpanded = false;

  constructor(public enrollmentService: EnrollmentService) {}

  // Hands-On 2: ngOnChanges fires every time an @Input changes
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['course']) {
      console.log('course input changed — previous:', changes['course'].previousValue,
                  '| current:', changes['course'].currentValue);
    }
  }

  onEnroll(): void {
    const courseId = this.course.id;
    if (this.enrollmentService.isEnrolled(courseId)) {
      this.enrollmentService.unenroll(courseId);
    } else {
      this.enrollmentService.enroll(courseId);
    }
    this.enrollRequested.emit(courseId);
  }

  toggleExpand(): void {
    this.isExpanded = !this.isExpanded;
  }

  // Hands-On 3: Getter keeps template clean — all class logic stays in TypeScript
  get cardClasses(): Record<string, boolean> {
    return {
      'card--enrolled': this.enrollmentService.isEnrolled(this.course?.id),
      'card--full': (this.course?.credits ?? 0) >= 4,
      'expanded': this.isExpanded
    };
  }

  // Hands-On 3: Dynamic border colour based on gradeStatus
  get borderColor(): string {
    const map: Record<string, string> = { passed: 'green', failed: 'red', pending: 'grey' };
    return map[this.course?.gradeStatus] ?? 'grey';
  }
}
