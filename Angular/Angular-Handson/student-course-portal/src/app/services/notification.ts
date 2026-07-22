import { Injectable } from '@angular/core';

@Injectable()
// NOTE: This service is NOT provided at root level — it is provided at component level
// (providers: [NotificationService] in @Component decorator).
// This creates a NEW separate instance scoped to that component and its children,
// rather than sharing the single root-level instance.
// Use case: isolated state per component, e.g. per-form notifications.
export class NotificationService {
  private messages: string[] = [];

  add(message: string): void {
    this.messages.push(message);
  }

  getAll(): string[] {
    return this.messages;
  }

  clear(): void {
    this.messages = [];
  }
}
