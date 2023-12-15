import { BrowserRouter, Routes, Route } from 'react-router-dom';
import React from 'react';
import './App.css';
import TaskTable from './Components/TaskTable';
import TaskForm from './Components/TaskForm';

export default function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path='/' element={<TaskTable />}/>
                <Route path='/newTask' element={<TaskForm />}/>
                <Route path='/editTask/:id' element={<TaskForm />}/>
            </Routes>
        </BrowserRouter>
    );
}