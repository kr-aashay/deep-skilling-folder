import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Store } from '@ngrx/store';
import { Course } from '../../models/course.model';
import { CourseService } from '../../services/course';
import { loadCourses } from '../../store/course/course.actions';
import { selectAllCourses, selectCoursesLoading, selectCoursesError } from '../../store/course/course.selectors';

@Component({
  selector: 'app-course-list',
  standalone: false,
  templateUrl: './course-list.html',
  styleUrl: './course-list.css'
})
export class CourseList implements OnInit {
  // Hands-On 9: NgRx observables — rendered with async pipe
  courses$!: Observable<Course[]>;
  isLoading$!: Observable<boolean>;
  error$!: Observable<string | null>;

  selectedCourseId: number | null = null;
  searchTerm = '';
  errorMessage = '';

  constructor(
    private courseService: CourseService,
    private router: Router,
    private store: Store
  ) {}

  ngOnInit(): void {
    // Hands-On 9: Dispatch action — NgRx Effect handles HTTP call
    this.store.dispatch(loadCourses());
    this.courses$    = this.store.select(selectAllCourses);
    this.isLoading$  = this.store.select(selectCoursesLoading);
    this.error$      = this.store.select(selectCoursesError);
  }

  // Hands-On 3: trackBy improves *ngFor performance — Angular only re-renders changed items
  // Without trackBy, Angular re-renders ALL items on any array change
  trackByCourseId(index: number, course: Course): number {
    return course.id;
  }

  onEnroll(courseId: number): void {
    console.log('Enrolling in course:', courseId);
    this.selectedCourseId = courseId;
  }

  onCardClick(course: Course): void {
    // Hands-On 7: Navigate to course detail with route parameter
    this.router.navigate(['courses', course.id]);
  }

  onSearch(): void {
    // Hands-On 7: Update URL query param on search
    this.router.navigate(['/courses'], { queryParams: { search: this.searchTerm } });
  }
}
