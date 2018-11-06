import * as React from 'react';
import Login from './Login';
import 'src/assets/css/App.css'



export default class Toogle extends  React.Component {

    public state = {
        on: false,
    }

    public toogle = () => {
        this.setState({
            on: !this.state.on
        })
    }
    public render() {
        return (
            <div className="logtoogle" >
                {this.state.on && (
                    <Login/>
                )}
                <div className="monte flex text-white text-right text-xl lg:flex-grow pt-4 pb-4">
                <button onClick={this.toogle} className="flex no-underline mt-4 lg:inline-block lg:mt-0 text-white mr-6">Zaloguj się</button>
                </div>
            </div>
        )
    }
}