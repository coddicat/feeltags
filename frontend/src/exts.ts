//import { validate as uuidValidate } from 'uuid';

import type { Location } from './api/question';

export function delay(ms: number): Promise<void> {
  return new Promise<void>(resolve => {
    setTimeout(resolve, ms);
  });
}

export function promiseWithTimeout<T>(
  promise: Promise<T>,
  ms: number
): Promise<T> {
  // Create a timeout promise that rejects after "ms" milliseconds
  const timeout = new Promise<T>((_, reject) => {
    const id = setTimeout(() => {
      clearTimeout(id);
      reject(`Timed out in ${ms}ms.`);
    }, ms);
  });

  // Returns a race between our timeout and the passed in promise
  return Promise.race([promise, timeout]);
}

export function getLocation(): Promise<Location | undefined> {
  const promise = new Promise<Location | undefined>((resolve, reject) => {
    if (!('geolocation' in navigator)) {
      resolve(undefined);
    }

    const options = {
      enableHighAccuracy: true, // Use GPS if available
      timeout: 5000, // Wait up to 10 seconds
      maximumAge: 60000 * 60 // Accept a cached position up to 60 minutes old
    };

    navigator.geolocation.getCurrentPosition(
      position => {
        resolve({
          latitude: position.coords.latitude,
          longitude: position.coords.longitude
        });
      },
      error => {
        reject(error);
      },
      options
    );
  });

  return promiseWithTimeout(promise, 5000);
}
