import * as React from "react";
import 'src/assets/css/App.css'
export default class Movies extends React.Component<any, any>{

public render() {

        return (
            <div className="text-left">
            <h3> Tytuł: {this.props.movie.title}</h3> 
            <h3> Opis: {this.props.movie.description}</h3> 
             </div>
           )
}


}
