import classNames from 'classnames';

export interface NewComponentProps {
    children: React.ReactNode;
}

/**
 * This component was created using Codux's Default new component template.
 * To create custom component templates, see https://help.codux.com/kb/en/article/configuration-for-new-components-and-templates
 */
export const NewComponent = ({}: NewComponentProps) => {
    return <div className={classNames('')}>NewComponent</div>;
};
