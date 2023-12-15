import * as React from "react";
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import './TaskTable.css'

function TaskTable() {
    const [tasks, setTasks] = useState();
    
    const [selectedTask, setSelectedRow] = useState(null);

    const handleRowClick = (rowId) => {
      setSelectedRow(rowId);
    };

    const navigate = useNavigate();

    useEffect(() => {
        populateTaskData();
    }, []);

    function redirectToTaskForm() {
        if (selectedTask) {
            navigate('editTask/' + selectedTask);
        }
        else {
            alert("Не выбрана ни одна задача для изменения!");
        }

    }

    function redirectToNewTaskForm() {
        navigate('newTask');
    }

    async function deleteData() {
        if (!selectedTask) {
            alert("Не выбрана ни одна задача для удаления!");
            return;
        }
        
        let filteredTasks = tasks.filter((element) => element.id != selectedTask);
        setTasks(filteredTasks)
        await fetch(`task?id=${selectedTask}`, {method:'DELETE'})
        .then((response) => {
            if (!response.ok)
                console.log(response.error)
        })
        .catch((error) => {
            console.log(error)
        });
    }

    const contents = tasks === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div><table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Status</th>
                </tr>
            </thead>
            <tbody>
                {tasks.map(task =>
                    <tr key={task.id} onClick={() => handleRowClick(task.id)} 
                        style= {{
                            cursor: "pointer",
                            backgroundColor: selectedTask === task.id ? 'lightblue' : ''
                        }}>
                        <td>{task.id}</td>
                        <td>{task.name}</td>
                        <td>{task.description}</td>
                        <td>{task.statusName}</td>
                    </tr>
                )}
            </tbody>
        </table>
        <div id="buttons">
            <button onClick={redirectToNewTaskForm}>Добавить</button>
            <button onClick={redirectToTaskForm}>Изменить</button>
            <button onClick={deleteData}>Удалить</button>
        </div></div>;
    return (
        <div>
            <h1 id="tabelLabel">Задачи</h1>
            {contents}
        </div>
    );

    async function populateTaskData() {
        const taskResponse = await fetch('task').then((response) => {
            if (response.ok)
                return response.json();
        })
        .catch((error) => {
            console.log(error)
        });

        setTasks(taskResponse);
    }
}

export default TaskTable;