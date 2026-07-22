import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { switchMap, map, catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { CourseService } from '../../services/course';
import { loadCourses, loadCoursesSuccess, loadCoursesFailure } from './course.actions';

@Injectable()
export class CourseEffects {
  // Effects are the ONLY place for side effects (HTTP, navigation, localStorage) in NgRx
  // Reducers must remain pure functions
  loadCourses$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadCourses),
      // switchMap cancels the previous inner Observable if a new action arrives
      switchMap(() =>
        this.courseService.getCourses().pipe(
          map(courses => loadCoursesSuccess({ courses })),
          catchError(error => of(loadCoursesFailure({ error: error.message })))
        )
      )
    )
  );

  constructor(private actions$: Actions, private courseService: CourseService) {}
}
