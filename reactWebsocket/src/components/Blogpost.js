import React, { useEffect, useState } from 'react'

export default function Blogpost({ title, authorName, date, link, id }) {
    const [theMap, setTheMap] = useState({});
    const [ws, setWs] = useState(undefined);

    const openSocket = () => {
        // Opening one Socket for each button-click. This can be done more efficient!
        const ws = new WebSocket("wss://localhost:49209/ws");
        setWs(ws);

        ws.onmessage = function (event) {
            const data = JSON.parse(event.data)
            console.log("Received: ", data);
            setTheMap(data);
        }

        setTimeout(() => {
            ws.send(link);
        }, 3000);
    };

    const closeSocket = () => {
        ws.close(1000, "Closing from client");
        setWs(undefined);
    };

    return (<li className='blogpost-container' key={id}>
        <a href={link} className='blogpost-title'>{title.rendered}</a>
        <span className='blogpost-author'>{authorName}</span>
        <span className='blogpost-date'>{date}</span>
        <details>
            <summary>
                <span>See word count</span>
                {ws ? <button onClick={closeSocket}>Close socket</button> : <button onClick={openSocket}>Open socket</button>}
            </summary>
            <ul>
                {Object.entries(theMap).map(entry => <li>{entry[0]}: {entry[1]}</li>)}
            </ul>
        </details> 
    </li>)
}