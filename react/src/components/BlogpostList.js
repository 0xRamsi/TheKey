import React from 'react'
import Blogpost from './Blogpost'

export default function BlogpostList({posts}){
    return posts.map(post => <Blogpost {...post} />)
}