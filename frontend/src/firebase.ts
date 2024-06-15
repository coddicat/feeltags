// Import the functions you need from the SDKs you need
import { initializeApp } from 'firebase/app';
import { getAnalytics } from 'firebase/analytics';
import { getAuth, GoogleAuthProvider } from 'firebase/auth';
// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
// For Firebase JS SDK v7.20.0 and later, measurementId is optional
const firebaseConfig = {
  apiKey: 'AIzaSyDAorxyKxkKgeKNzYFSbszEYb7tNryHMjE',
  authDomain: 'feeltags.firebaseapp.com',
  projectId: 'feeltags',
  storageBucket: 'feeltags.appspot.com',
  messagingSenderId: '996088302247',
  appId: '1:996088302247:web:a34e1dffcc0e93f3912982',
  measurementId: 'G-BH9SWRLMWK'
};

// Initialize Firebase
const app = initializeApp(firebaseConfig);
getAnalytics(app);
const auth = getAuth();
auth.languageCode = 'it';
const provider = new GoogleAuthProvider();
provider.addScope('https://www.googleapis.com/auth/userinfo.email');

export const googleAuth = auth;
export const googleProvider = provider;
