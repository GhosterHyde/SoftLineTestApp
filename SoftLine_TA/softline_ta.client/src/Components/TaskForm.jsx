import React, {Component} from "react"
import { useNavigate, useParams } from 'react-router-dom'
import './TaskForm.css'

function withParams(Component) {
    return props => <Component {...props} {...{ navigate: useNavigate(), params: useParams() }} />;
}

class TaskForm extends Component {
    componentDidMount() {
        let { id } = this.props.params;
        this.getTask(id);
    }

    constructor(props){
        super(props);
        this.state = {
            id: 0,
            name: '',
            descValue: '',
            status: 0
        }
        this.onSubmit = this.onSubmit.bind(this);
        this.handleNameChange = this.handleNameChange.bind(this);
        this.handleDescChange = this.handleDescChange.bind(this);
        this.handleStateChange = this.handleStateChange.bind(this);
        this.navigateBack = this.navigateBack.bind(this);
        this.getStatuses();
    }

    handleNameChange(e) {
        this.setState({
            name: e.target.value
        });
    }

    handleDescChange(e) {
        this.setState({
            descValue: e.target.value
        });
    }

    handleStateChange(e) {
        this.setState({
            status: e.target.value
        });
    }

    navigateBack() {
        this.props.navigate('../');
    }

    async getStatuses() {
        const statusesResponse = await fetch('../statuses').then((response) => {    
            if (response.ok)
                return response.json();
        })
        .catch((error) => {
            console.log(error)
        });

        this.statuses = statusesResponse;

        if (statusesResponse.length > 0) {
            this.setState({
                status: statusesResponse[0].statusId
            });
        }
    }

    async getTask(id) {
        if (id) {
            const taskResponse = await fetch('../task/' + id).then((response) => {
                if (response.ok)
                    return response.json();
            })
            .catch((error) => {
                console.log(error)
            });
            
            this.setState({
                id: id,
                name: taskResponse.name,
                descValue: taskResponse.description,
                status: taskResponse.statusId
            })
        }
    }

    async onSubmit(e) {
        e.preventDefault();
        if (this.state.name.length == 0 || this.state.descValue.length == 0) {
            alert("Заполните все поля!");
            return;
        }
        
        var route = `../task/`;
        route += this.state.id === 0 ? "create" : "update";
        var method = this.state.id === 0 ? "POST" : "PUT";
        await fetch(route,
            {
                method: method,
                headers: {'Content-Type': 'application/json'},
                body: JSON.stringify({
                    id: +this.state.id,
                    name: this.state.name,
                    description: this.state.descValue,
                    statusId: +this.state.status,
                })
            })
            .catch((error) => {
                console.log(error)
            });

            this.navigateBack();
    }

    render() {
        return(
            <form onSubmit={this.onSubmit}>
                <h1>Задача</h1>
                <div id="form">
                    <label htmlFor="name">Название</label>
                    <input id="name" value={this.state.name} onChange={this.handleNameChange}></input>
                    <label htmlFor="desc">Описание</label>
                    <textarea id="desc" value={this.state.descValue} onChange={this.handleDescChange}></textarea>
                    <label htmlFor="status">Статус</label>
                    <select id="status" onChange={this.handleStateChange} value={this.state.status}>
                        {this.statuses?.map(status => 
                            (<option key={status.statusId} value={status.statusId}>{status.statusName}</option>)
                        )}
                    </select>
                </div>
                <button id="submitButton" type="submit">Отправить</button>
                <button onClick={this.navigateBack}>Назад</button>
            </form>
        );
    }
}

export default withParams(TaskForm);