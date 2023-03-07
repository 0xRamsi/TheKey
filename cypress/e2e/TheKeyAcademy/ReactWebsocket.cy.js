/// <reference types="cypress" />

const checkWordCountForBlogpost = (container, fixtureName) => {
  cy.fixture(fixtureName).then(data => {
    container.within(() => {
      cy.get('button').click();
      cy.get('summary').click();
      cy.wait(10000);   // Sadly, it's not possible to observe websocket communication
      Object.entries(data).forEach((value, key) => {
        cy.contains(value[0] +': ' + value[1]);
      })
    })
  })

}

describe('Blogpost word count', () => {
  beforeEach(() => {
    cy.visit('http://localhost:1000/')
  })

  it('check that blogposts are loaded', () => {
    cy.intercept('**/GetBlogData', { fixture: 'blogposts.json'}).as('getBlogposts')

    cy.visit('http://localhost:1000/')
    cy.wait('@getBlogposts')

    cy.get('.blogpost-container').should('have.length', 3)
    
    cy.get('.blogpost-container').first().contains('Blogpost 1')
    cy.get('.blogpost-container').last().contains('Blogpost 3')
  })

  it('reads word count of first two blogposts', () => {
    cy.intercept('**/GetBlogData').as('getBlogposts')
    cy.intercept('**/ws').as('getWebsocket')

    cy.wait('@getBlogposts');

    const fixtures = ['konfliktmanagement.json', '']
    cy.get('.blogpost-container').forEach((container, index) => {
      checkWordCountForBlogpost(container, fixtures[index])
    });
  })
    
})
