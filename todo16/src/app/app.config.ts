import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

// change into standalone module-less HTTPClient
const HttpProviders = [
  provideHttpClient(
    // do this, to keep using your class-based interceptors.
    withInterceptorsFromDi()
  )
];
export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes), HttpProviders ]
};
