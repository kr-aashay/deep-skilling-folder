import { createFeatureSelector, createSelector } from '@ngrx/store';
import { CourseState } from './course.reducer';

// Selectors are memoised — only recompute when input selectors change (key NgRx perf optimisation)
export const selectCourseState  = createFeatureSelector<CourseState>('course');
export const selectAllCourses   = createSelector(selectCourseState, s => s.courses);
export const selectCoursesLoading = createSelector(selectCourseState, s => s.loading);
export const selectCoursesError   = createSelector(selectCourseState, s => s.error);
