import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'

export function mountApp(rootElement) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(<App />);
};
