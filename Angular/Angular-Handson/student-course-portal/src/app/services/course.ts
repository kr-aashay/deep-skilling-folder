import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap, retry } from 'rxjs/operators';
import { Course } from '../models/course.model';

// providedIn: 'root' makes this a singleton — one instance shared across the entire app
@Injectable({ providedIn: 'root' })
export class CourseService {
  private apiUrl = 'http://localhost:3000/courses';

  // Hands-On 6: In-memory fallback data (used before HTTP setup in Hands-On 8)
  private courses: Course[] = [
    { id: 1, name: 'Data Structures', code: 'CS101', credits: 4, gradeStatus: 'passed', enrolled: true },
    { id: 2, name: 'Algorithms', code: 'CS102', credits: 4, gradeStatus: 'pending', enrolled: false },
    { id: 3, name: 'Web Development', code: 'WD201', credits: 3, gradeStatus: 'passed', enrolled: true },
    { id: 4, name: 'Database Systems', code: 'DB301', credits: 3, gradeStatus: 'failed', enrolled: false },
    { id: 5, name: 'Operating Systems', code: 'OS401', credits: 4, gradeStatus: 'pending', enrolled: false }
  ];

  constructor(private http: HttpClient) {}

  // Hands-On 8: HTTP GET with RxJS operators
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(this.apiUrl).pipe(
      // tap: side effect for logging — does NOT modify the stream (unlike map)
      tap(courses => console.log('Courses loaded:', courses.length)),
      // map: filter only valid courses
      map(courses => courses.filter(c => c.credits > 0)),
      // retry: attempt up to 2 times before propagating error
      retry(2),
      catchError(err => {
        console.error('HTTP failed, falling back to local data:', err.message);
        // Fall back to local data when JSON server is not running
        return of(this.courses);
      })
    );
  }

  getCourseById(id: number): Observable<Course | undefined> {
    return this.http.get<Course>(`${this.apiUrl}/${id}`).pipe(
      catchError(() => of(this.courses.find(c => c.id === id)))
    );
  }

  // Hands-On 8: POST
  createCourse(course: Omit<Course, 'id'>): Observable<Course> {
    return this.http.post<Course>(this.apiUrl, course).pipe(
      catchError(() => {
        const newCourse = { ...course, id: Date.now() } as Course;
        this.courses.push(newCourse);
        return of(newCourse);
      })
    );
  }

  // Hands-On 8: PUT
  updateCourse(id: number, course: Partial<Course>): Observable<Course> {
    return this.http.put<Course>(`${this.apiUrl}/${id}`, course).pipe(
      catchError(() => {
        const idx = this.courses.findIndex(c => c.id === id);
        if (idx !== -1) this.courses[idx] = { ...this.courses[idx], ...course };
        return of(this.courses[idx]);
      })
    );
  }

  // Hands-On 8: DELETE
  deleteCourse(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`).pipe(
      catchError(() => {
        this.courses = this.courses.filter(c => c.id !== id);
        return of(void 0);
      })
    );
  }

  // Hands-On 6: Synchronous helpers for component-level access
  getCoursesLocal(): Course[] { return this.courses; }
  getCourseByIdLocal(id: number): Course | undefined { return this.courses.find(c => c.id === id); }
  addCourse(course: Course): void { this.courses.push(course); }
}
