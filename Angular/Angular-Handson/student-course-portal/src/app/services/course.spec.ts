import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { CourseService } from './course';
import { Course } from '../models/course.model';

const mockCourses: Course[] = [
  { id: 1, name: 'Data Structures', code: 'CS101', credits: 4, gradeStatus: 'passed' },
  { id: 2, name: 'Algorithms',       code: 'CS102', credits: 4, gradeStatus: 'pending' }
];

describe('CourseService', () => {
  let service: CourseService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CourseService]
    });
    service  = TestBed.inject(CourseService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    // Asserts no unexpected HTTP requests were made
    httpMock.verify();
  });

  // Test 1: getCourses returns expected courses
  it('should return courses from the API', () => {
    service.getCourses().subscribe(courses => {
      expect(courses.length).toBe(2);
      expect(courses[0].name).toBe('Data Structures');
    });

    const req = httpMock.expectOne('http://localhost:3000/courses');
    expect(req.request.method).toBe('GET');
    req.flush(mockCourses);
  });

  // Test 2: error handling — falls back to local data on HTTP failure
  it('should fall back to local data on HTTP error', () => {
    service.getCourses().subscribe(courses => {
      expect(courses.length).toBeGreaterThan(0);
    });

    const req = httpMock.expectOne('http://localhost:3000/courses');
    // Flush a 500 error — catchError in service handles it
    req.flush('Server Error', { status: 500, statusText: 'Internal Server Error' });
  });
});
