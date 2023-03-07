import React, { useEffect, useState } from 'react'
import { useContext } from 'react';
import { PortContext } from '../context/PortContext.js';

export default function Blogpost({ title, authorName, date, link, id }) {
    const [theMap, setTheMap] = useState({});
    const [ws, setWs] = useState(undefined);
    const port = useContext(PortContext);

    const openSocket = () => {
        // Opening one Socket for each button-click. This can be done more efficient!
        const ws = new WebSocket(`ws://localhost:${port}/ws`);
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
        <span className='blogpost-author-date'>{authorName} {date.replace('T', ' ').slice(0, 16)}</span>
        <details className='blogpost-wordCount'>
            <summary>
                <span>See word count</span>
                {ws ? <button onClick={closeSocket}>Close socket</button> : <button onClick={openSocket}>Get word-count with a socket</button>}
            </summary>
            <ul>
                {Object.entries(theMap).map(entry => <li>{entry[0]}: {entry[1]}</li>)}
            </ul>
        </details> 
    </li>)
}