describe('Matching Main', function () {

    // Makes sure activity button and route work
    it('Goes to Activity Profile', function () {

        cy.visit('https://localhost:5002')

        cy.contains('Test').click()

        cy.contains('Matching').click()

        cy.contains('Activity Profile').click()

        cy.contains('Back').click()
    })

    // Makes sure tutoring profile and route work
    it('Goes to tutoring profile', function () {

        cy.visit('https://localhost:5002/matchingmain')

        cy.contains('Tutoring Profile').click()

        cy.contains('Back').click()

    })

    // Generates matches
    it('Generates matches', function () {

        cy.visit('https://localhost:5002/matchingmain')

        cy.contains('Generate Activity Matches').click()

        cy.contains('Generate Tutoring Matches').click()

    })

    // Opt in / Opt out of matching 
    it('Selects opt in/ opt out', function () {
        cy.visit('https://localhost:5002/matchingmain')

        cy.contains('YOU ARE OPTED IN OF MATCHING').click()

        cy.contains('YOU ARE OPTED OUT OF MATCHING').click()

    })

    // Return to homepage 
    it('Returns to homepage', function () {
        cy.visit('https://localhost:5002/matchingmain')

        cy.contains('Return to Homepage').click()

    })
})