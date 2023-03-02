import React from 'react'

export default function Blogpost({ title, authorName, date, link, id, theMap }) {
    return (<li className='blogpost-container' key={id}>
        <a href={link} className='blogpost-title'>{title.rendered}</a>
        <span className='blogpost-author'>{authorName}</span>
        <span className='blogpost-date'>{date}</span>
        <details>
            <summary>See word count</summary>
            <ul>
                {Object.entries(theMap).map(entry => <li>{entry[0]}: {entry[1]}</li>)}
            </ul>
        </details> 
    </li>)
}