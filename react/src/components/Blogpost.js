import React from 'react'

export default function Blogpost({ title, authorName, date, link }) {
    return (<li className='blogpost-container'>
        <a href={link} className='blogpost-title'>{title.rendered}</a>
        <span className='blogpost-author'>{authorName}</span>
        <span className='blogpost-date'>{date}</span>
    </li>)
}