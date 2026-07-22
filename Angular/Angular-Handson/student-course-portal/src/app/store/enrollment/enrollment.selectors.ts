import { createFeatureSelector, createSelector } from '@ngrx/store';
import { EnrollmentState } from './enrollment.reducer';
import { selectAllCourses } from '../course/course.selectors';

export const selectEnrollmentState = createFeatureSelector<EnrollmentState>('enrollment');

export const selectEnrolledIds = createSelector(
  selectEnrollmentState,
  s => s.enrolledCourseIds
);

// Cross-slice selector — combines course state + enrollment state
// Derives enrolled Course objects without duplicating data in the store
export const selectEnrolledCourses = createSelector(
  selectAllCourses,
  selectEnrolledIds,
  (courses, ids) => courses.filter(c => ids.includes(c.id))
);
