import type {SearchBarProps} from "@/features/search/search-bar/SearchBar.types.ts";
import type {KeyboardEvent} from 'react';
import {Input} from '@/shared/ui/input';
import {Button} from '@/shared/ui/button';
import styles from "./SearchBar.module.css";
import SearchIcon from "@/shared/assets/icons/search.svg?react";
import CrossIcon from "@/shared/assets/icons/cross.svg?react";


export const SearchBar = (
    {
        value = '',
        onChange,
        onSearch,
        onClear,
        placeholder,
        className,
    }: SearchBarProps) => {

    const hasValue = value.length > 0;

    const handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
        if (event.key === 'Enter') {
            onSearch?.(value);
        }
    };

    const searchBarStyles = [
        styles.searchBar,
        className,
    ].filter(Boolean).join(' ');

    const deleteButtonStyles = [
        styles.deleteButton,
        hasValue ? null : styles.deleteButton__hidden,

    ].filter(Boolean).join(' ');

    return (
        <div className={searchBarStyles}>
            <div className={styles.wrapper}>
                <Input
                    className={styles.input}
                    value={value}
                    placeholder={placeholder}
                    onChange={onChange}
                    onKeyDown={handleKeyDown}
                />

                <Button
                    className={deleteButtonStyles}
                    variant="ghost"
                    size="medium"
                    icon={<CrossIcon/>}
                    onClick={() => onClear?.()}
                />
            </div>

            <Button
                className={styles.searchButton}
                variant="primary"
                size="medium"
                icon={<SearchIcon/>}
                onClick={() => onSearch?.(value)}
            />
        </div>
    );
};